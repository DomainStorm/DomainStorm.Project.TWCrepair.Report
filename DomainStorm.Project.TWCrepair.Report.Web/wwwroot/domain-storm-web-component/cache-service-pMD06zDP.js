const DEFAULT_TTL_MS = 600_000;
class CacheService {
    provider;
    keyPrefix;
    defaultTtlMs;
    constructor(provider, options = {}) {
        this.provider = provider;
        this.keyPrefix = options.regionName ?? provider.defaultKeyPrefix ?? 'cache';
        this.defaultTtlMs = options.ttlMillis ?? DEFAULT_TTL_MS;
        if (options.maxEntries && provider.setMaxEntries) {
            provider.setMaxEntries(options.maxEntries);
        }
    }
    getFullKey(key) {
        return this.keyPrefix + ':' + key;
    }
    isExpired(entry) {
        const ttl = (typeof entry.ttlMillis === 'number' && !isNaN(entry.ttlMillis))
            ? entry.ttlMillis
            : this.defaultTtlMs;
        return (Date.now() - entry.timestamp) > ttl;
    }
    async get(key) {
        const fullKey = this.getFullKey(key);
        const entry = await this.provider.get(fullKey);
        if (!entry)
            return null;
        if (this.isExpired(entry)) {
            // Fire and forget deletion for expired entries
            this.provider.delete(fullKey).catch(() => { });
            return null;
        }
        return entry.value;
    }
    async set(key, value, ttlMs = this.defaultTtlMs) {
        const fullKey = this.getFullKey(key);
        const entry = {
            value,
            timestamp: Date.now(),
            ttlMillis: ttlMs,
        };
        await this.provider.set(fullKey, entry);
    }
    async delete(key) {
        const fullKey = this.getFullKey(key);
        await this.provider.delete(fullKey);
    }
    async clear() {
        await this.provider.clear();
    }
    async size() {
        return await this.provider.size();
    }
    async destroy() {
        if (this.provider.destroy) {
            await this.provider.destroy();
        }
        else {
            await this.provider.clear();
        }
    }
}

export { CacheService as C };
//# sourceMappingURL=cache-service-pMD06zDP.js.map

//# sourceMappingURL=cache-service-pMD06zDP.js.map