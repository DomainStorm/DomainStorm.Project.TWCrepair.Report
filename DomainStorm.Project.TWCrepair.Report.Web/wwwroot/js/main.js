window.getJSObjectReference = (element) => element;

window.setProperty = (element, key, value) => element[key] = JSON.parse(value);

window.getProperty = (element, key) => element[key];

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

window.drawChart = async (target, jsonString) => {

    var json = JSON.parse(jsonString);

    Plotly.react(target, json.data, json.layout, { displayModeBar: false });
}

window.drawImage = async function (target, jsonString) {
    const json = typeof jsonString === 'string' ? JSON.parse(jsonString) : jsonString;
    const container = document.getElementById(target);
    if (!container) return;

    // 1) 呼叫 Plotly.react（可能回傳 Promise 或 not）
    let plotlyResult;
    try {
        plotlyResult = Plotly.react(container, json.data, json.layout, { displayModeBar: false });
    } catch (e) {
        // 如果 Plotly 抛例外，仍繼續讓外層知道失敗
        console.error('Plotly.react error', e);
        throw e;
    }

    // 2) 確保圖表已渲染完成：如果 plotlyResult 有 then，就 await；否則等 plotly_afterplot 事件
    if (plotlyResult && typeof plotlyResult.then === 'function') {
        await plotlyResult;
    } else {
        await new Promise((resolve) => {
            const handler = () => {
                container.removeEventListener('plotly_afterplot', handler);
                resolve();
            };
            container.addEventListener('plotly_afterplot', handler);
            // 安全保險：若事件沒觸發，在 2 秒後自動解決
            setTimeout(() => {
                container.removeEventListener('plotly_afterplot', handler);
                resolve();
            }, 2000);
        });
    }

    // 3) 轉成 image（可能會因跨域資源失敗）
    const dataUrl = await Plotly.toImage(container, { format: 'png', scale: 1 });

//    // 4) 建立並插入圖片，清除原圖表容器（或依需求改行為）
//    const img = document.createElement('img');
//    img.src = dataUrl;
//    img.style.border = "1px solid #ccc";
//    img.style.marginTop = "20px";
//
//    const imageContainer = document.getElementById(imageTarget);
//    if (imageContainer) {
//        imageContainer.innerHTML = '';
//        imageContainer.appendChild(img);
//    }
    container.innerHTML = "";
    // 回傳 dataUrl（或只是用來讓 Blazor await 完成）
    return dataUrl;
};