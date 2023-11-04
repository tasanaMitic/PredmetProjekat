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

const addCategory = (body) => {
    return post('/api/category', body);
}

//brand
const deleteBrand = (brandId) => {
    return remove('/api/brand/' + brandId);
}

const getBrands = () => {
    return get('/api/brand');
}

const addBrand = (body) => {
    return post('/api/brand', body);
}

//product
const getProducts = () => {
    return get('/api/product/all');
}

const getStockedProducts = () => {
    return get('/api/product/stocked');
}

const deleteProduct = (productId) => {
    return remove('/api/product/' + productId);
}

const createProduct = (body) => {
    return post('/api/product', body);
}

const stockProduct = (productId, body) => {
    return patch('/api/product/' + productId, body);
}

//proptypes
stockProduct.propTypes = {
    productId: PropTypes.string,
    body: PropTypes.shape({
        value: PropTypes.number,
    })
}

createProduct.propTypes = {
    body: PropTypes.shape({
        name: PropTypes.string,
        size: PropTypes.string,
        sex: PropTypes.string,
        season: PropTypes.string,
        categoryId: PropTypes.number,
        brandId: PropTypes.number
    })
}

addBrand.propTypes = {
    body: PropTypes.string
}

deleteProduct.propTypes = {
    productId: PropTypes.number
}

addCategory.propTypes = {
    body: PropTypes.string
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
    getAdmins, getEmployees, getCategories, getBrands, getUser, getProducts, getStockedProducts,
    deleteEmployee, deleteCategory, deleteBrand, deleteProduct,
    assignManager, updateUser, stockProduct,
    addCategory, addBrand, createProduct
};