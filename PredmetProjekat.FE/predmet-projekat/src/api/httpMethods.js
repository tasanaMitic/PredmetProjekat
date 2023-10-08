import api from './server'
import Cookies from 'universal-cookie';

const post = (url, body) => {
    return api.post(url, body, {
        headers: {
            'Content-Type': 'application/json', //todo constants                // + za login mi ne treba bearer pa srediti to
            'Authorization' : 'Bearer ' + new Cookies().get("jwt_authorization")
        }
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
        headers: {
            'Content-Type': 'application/json',
            'Authorization' : 'Bearer ' + new Cookies().get("jwt_authorization")
        }
    });
}

const remove = (url) => {
    return api.delete(url, {
        headers: {
            'Content-Type': 'application/json',
            'Authorization' : 'Bearer ' + new Cookies().get("jwt_authorization")
        }
    });
}

const patch = (url, body) => {
    return api.patch(url, body, {
        headers: {
            'Authorization' : 'Bearer ' + new Cookies().get("jwt_authorization")
        }
    });
}

export { post, get, remove, postWOToken, patch };