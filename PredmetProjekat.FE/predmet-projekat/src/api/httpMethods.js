import api from './server'
import Cookies from 'universal-cookie';

const post = (url, body) => {
    return api.post(url, body, {
        headers: setHeaders()
    });
}

const postWOToken = (url, body) => {
    return api.post(url, body, {
        headers: {
            'Content-Type': 'application/json'
        }
    });
}

const get = (url) => {
    return api.get(url, {
        headers: setHeaders()
    });
}

const remove = (url) => {
    return api.delete(url, {
        headers: setHeaders()
    });
}

const patch = (url, body) => {
    return api.patch(url, body, {
        headers: setHeaders()
    });
}

const setHeaders = () => {
    return {
        'Content-Type': 'application/json',
        'Authorization' : 'Bearer ' + new Cookies().get("jwt_authorization")
    };
}

export { post, get, remove, postWOToken, patch };