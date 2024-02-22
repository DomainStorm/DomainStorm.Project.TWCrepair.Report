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