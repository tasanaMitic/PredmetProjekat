import { post, get, remove, postWOToken, patch } from './httpMethods'

const login = (body) => {
    return postWOToken('/api/account/login', body); //todo constants
}

const register = (body, userType) => {
    return post('/api/account/' + userType, body);
}

const assignManager = (body) => {
    return patch('/api/employee', body);
}

const getAdmins = () => {
    return get('/api/admin');
}

const getEmployees = () => {
    return get('/api/employee/all');
}

const getCategories = () => {
    return get('/api/category');
}

const getBrands = () => {
    return get('/api/brand');
}

const deleteEmployee = (username) => {
    return remove('/api/employee/' + username);
}



export { login, register, getAdmins, getEmployees, getCategories, getBrands, deleteEmployee, assignManager };