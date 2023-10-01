import api from './server'

const post = (url, body) => {
    api.post(url, body, {
        headers: {
            'Content-Type': 'application/json', //todo constants
        }
    }).then(function (response) {
        console.log('post http');
        console.log('post http' + response);
        return response;
    }).catch(function (error) {
        console.log(error);
        return error;
    });
}

const get = (url) => {
    api.get(url, {
        headers: {
            'Content-Type': 'application/json',
        }
    }).then(function (response) {
        return response;
    }).catch(function (error) {
        return error;
    });
}

const remove = (url) => {
    api.delete(url, {
        headers: {
            'Content-Type': 'application/json',
        }
    }).then(function (response) {
        return response;
    }).catch(function (error) {
        return error;
    });
}

export { post, get, remove };