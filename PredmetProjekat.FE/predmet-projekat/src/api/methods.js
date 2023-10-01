import { post, get, remove } from './httpMethods'

const login = (body) => {
    return post('/api/account/login', body);
}

const register = (body, userType) => {
    return post('/api/account/' + userType, body);
}

export { login, register };