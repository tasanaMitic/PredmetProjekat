import api from './server'
import Cookies from 'universal-cookie';

const post = (url, body) => {
    return api.post(url, body, {
        headers: {
            'Content-Type': 'application/json', //todo constants
            'Authorization' : new Cookies().get("jwt_authorization")
        }
    });
}

const get = (url) => {
    api.get(url, {
        headers: {
            'Content-Type': 'application/json',
            'Authorization' : new Cookies().get("jwt_authorization")
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