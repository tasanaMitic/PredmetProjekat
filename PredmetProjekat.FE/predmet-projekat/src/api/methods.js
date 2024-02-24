import { post, get, remove, postWOToken, patch, put } from './httpMethods';
import PropTypes from 'prop-types';

//user
const login = (body) => {
    return postWOToken('/api/account/login', body);
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

//registers
const addRegister = (body) => {
    return post('/api/register', body);
}

const getRegisters = () => {
    return get('/api/register');
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
    return patch('/api/product/stock/' + productId, body);
}

const setProductPrice = (productId, body) => {
    return patch('/api/product/price/' + productId, body);
}

const sellProduct = (body) => {
    return patch('/api/product/', body);
}

//sales
const getSalesForUser = () => {
    return get('/api/product/sales');
}

const getAllSales = () => {
    return get('/api/product/allsales');
}

const getFilteredSales = (queryParams) => {    
    return get('/api/product/filter' + queryParams);
}

//productTypes
const getProductTypes = () => {
    return get('/api/producttype');
}

const createProductType = (body) => {
    return post('/api/producttype', body);
}

const deleteProductType = (productTypeId) => { 
    return remove('/api/producttype/' + productTypeId);
}


//proptypes
deleteProductType.propTypes = {
    productTypeId: PropTypes.number
}

getFilteredSales.propTypes = {
    queryParams: PropTypes.shape({
        registerLocations: PropTypes.arrayOf(PropTypes.string),
        registerCodes: PropTypes.arrayOf(PropTypes.string),
        emplyeeUsernames: PropTypes.arrayOf(PropTypes.string),
        saleDates: PropTypes.arrayOf(PropTypes.string),
    })
}

createProductType.propTypes = {
    body: PropTypes.shape({
        name: PropTypes.string,
        attributes: PropTypes.arrayOf(PropTypes.string)
    })
}

addRegister.propTypes = {
    body: PropTypes.shape({
        registerCode: PropTypes.string,
        location: PropTypes.string
    })
}

setProductPrice.propTypes = {
    productId: PropTypes.string,
    body: PropTypes.shape({
        value: PropTypes.number,
    })
}

sellProduct.propTypes = {
    body: PropTypes.shape({
        registerId: PropTypes.number,
        soldProducts: PropTypes.arrayOf(PropTypes.shape({
            productId: PropTypes.string,
            quantity: PropTypes.number
        }))
    })
}

stockProduct.propTypes = {
    productId: PropTypes.string,
    body: PropTypes.shape({
        value: PropTypes.number,
    })
}

createProduct.propTypes = {
    body: PropTypes.shape({
        name: PropTypes.string,
        categoryId: PropTypes.number,
        brandId: PropTypes.number,
        attributeValues: PropTypes.arrayOf(PropTypes.shape({
            attributeId: PropTypes.number,
            attributeValue: PropTypes.string
        }))
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
    body: PropTypes.shape({
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        email: PropTypes.string,
        username: PropTypes.string,
        password: PropTypes.string,
    })
}
assignManager.propTypes = {
    body: PropTypes.shape({
        managerUsername: PropTypes.string,
        employeeUsername: PropTypes.string,
    })
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
    getAdmins, getEmployees, getCategories, getBrands, getUser, getProducts, getStockedProducts, getRegisters, getSalesForUser, getAllSales, getProductTypes, getFilteredSales,
    deleteEmployee, deleteCategory, deleteBrand, deleteProduct, deleteProductType,
    assignManager, updateUser, stockProduct, sellProduct, setProductPrice,
    addCategory, addBrand, createProduct, addRegister, createProductType
};