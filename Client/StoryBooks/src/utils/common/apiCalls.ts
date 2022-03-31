const sendGet = (url: string) => {
    return sendRequest('GET', url, null);
}

const sendPut = (url: string, data: any) => {
    return sendRequest('PUT', url, data);
}

const sendPost = (url: string, data: any) => {
    return sendRequest('POST', url, data);
}

const sendDelete = (url: string, data: any) => {
    return sendRequest('DELETE', url, data);
}

const sendRequest = (method: string, url: string, data: any) => {
    return fetch(url, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data),
      })
}

export {
    sendGet,
    sendPost,
    sendPut,
    sendDelete
};