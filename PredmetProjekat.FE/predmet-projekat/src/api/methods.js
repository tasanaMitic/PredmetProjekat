import { post, get, remove, postWOToken, patch, put } from './httpMethods';
import PropTypes from 'prop-types';

//user
const login = (body) => {
    return postWOToken('/api/account/login', body); //todo constants, propTypes
}

const register = (userRole, body) => {
    return post('/api/account/' + userRole, body);
}

const assignManager = (body) => {
    return patch('/api/employee', body);
}

const getAdmins = () => {
    return get('/api/admin/all');
}

const getUser = (userRole, username) => {
    return get('/api/' + userRole + '/' + username);
}

const updateUser = (userRole, body) => {
    return put('/api/' + userRole, body);
}

const getEmployees = () => {
    return get('/api/employee/all');
}

const deleteEmployee = (username) => {
    return remove('/api/employee/' + username);
}

//category
const deleteCategory = (categoryId) => {
    return remove('/api/category/' + categoryId);
}

const getCategories = () => {
    return get('/api/category');
}

const addCategory = (payload) => {
    return post('/api/category', payload);
}

//brand
const deleteBrand = (brandId) => {
    return remove('/api/brand/' + brandId);
}

const getBrands = () => {
    return get('/api/brand');
}

const addBrand = (payload) => {
    return post('/api/brand', payload);
}

//proptypes
addBrand.propTypes = {
    payload: PropTypes.string
}

addCategory.propTypes = {
    payload: PropTypes.string
}

deleteBrand.propTypes = {
    brandId: PropTypes.number
}

deleteCategory.propTypes = {
    categoryId: PropTypes.number
}

deleteEmployee.propTypes = {
    username: PropTypes.string
}
getUser.propTypes = {
    username: PropTypes.string,
    userRole: PropTypes.string
}
register.propTypes = {
    userRole: PropTypes.string,
    body: PropTypes.object  //todo
}
assignManager.propTypes = {
    body: PropTypes.object  //todo
}
login.propTypes = {
    body: PropTypes.shape({
        email: PropTypes.email,
        password: PropTypes.password
    })
}
updateUser.propTypes = {
    userRole: PropTypes.string,
    body: PropTypes.shape({
        username: PropTypes.string,
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        email: PropTypes.string,
    })
}



export {
    login, register,
    getAdmins, getEmployees, getCategories, getBrands, getUser,
    deleteEmployee, deleteCategory, deleteBrand,
    assignManager, updateUser,
    addCategory, addBrand
};