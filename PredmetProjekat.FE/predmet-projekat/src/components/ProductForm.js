import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { getBrands, getCategories, createProduct, getProductTypes } from "../api/methods";
import { useHistory } from "react-router-dom";
import ModalError from "./modals/ModalError";
import ModalSuccess from "./modals/ModalSuccess";

const ProductForm = () => {
    const history = useHistory();
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);
    const [successModal, setSuccessModal] = useState(false);
    const [brands, setBrands] = useState(null);
    const [categories, setCategories] = useState(null);
    const [productTypes, setProductTypes] = useState(null);

    const [name, setName] = useState('');
    const [brand, setBrand] = useState(null);
    const [category, setCategory] = useState(null);
    const [productType, setProductType] = useState(null);
    const [attributes, setAttributes] = useState([]);

    useEffect(() => {
        getBrands().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then((data) => {
            setBrands(data);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })

        getCategories().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then((data) => {
            setCategories(data);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })

        getProductTypes().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then((data) => {
            setProductTypes(data);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })


    }, [])

    const handleBrandChange = (brandId) => {
        setBrand(brandId === 'default' ? null : brandId);
    }

    const handleCategoryChange = (categoryId) => {
        setCategory(categoryId === 'default' ? null : categoryId);
    }

    const handleChange = (attributeId, attributeValue) => {
          setAttributes(prevArray => [
            ...prevArray.filter(item => item.attributeId !== attributeId),
            { attributeId, attributeValue },
          ]);
    };

    const handleProductTypeChange = (productTypeId) => {
        setAttributes([]);
        setProductType(productTypeId === 'default' ? null : findElement(productTypeId));
    }

    const findElement = (productTypeId) => {
        return productTypes.find((element) => {
            return element.productTypeId === productTypeId;
        })
    }

    const handleSave = () => {
        const payload = {
            name: name,
            categoryId: category,
            brandId: brand, 
            attributeValues: attributes, 
            productTypeId: productType.productTypeId
        };
        
        console.log(payload);

        createProduct(payload).then(res => {
            if (res.status !== 201) {
                throw Error('There was an error with the request!');
            }
            setSuccessModal(true);
            setSuccessMessage("You have successfully created a product with name " + name);
            setError(null);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })
    }

    const handleCancel = () => {
        history.replace('/products');
    }

    return (
        <Form>
            <ModalSuccess setShow={setSuccessModal} show={successModal} clearData={handleCancel} message={successMessage} />
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
            <Form.Group className="mb-3" controlId="formBasicName" key="formBasicName" >
                <Form.Label>Name</Form.Label>
                <Form.Control type="name" placeholder="Enter name" value={name} onChange={(e) => setName(e.target.value)} required />
            </Form.Group>
            {categories &&
                <Form.Group className="mb-3" controlId="formBasicCategory" key="formBasicCategory" >
                    <Form.Label>Category</Form.Label>
                    {categories.length > 0 ?
                        <Form.Select aria-label="Default select example" onChange={(e) => handleCategoryChange(e.target.value)}>
                            <option value="default" key="default">Select category</option>
                            {categories.map((category) => <option value={category.categoryId} key={category.categoryId}>{category.name}</option>)}
                        </Form.Select> : <p value="default">There are no available categories.</p>}
                </Form.Group>
            }
            {brands &&
                <Form.Group className="mb-3" controlId="formBasicBrand" key="formBasicBrand" >
                    <Form.Label>Brand</Form.Label>
                    {brands.length > 0 ?
                        <Form.Select aria-label="Default select example" onChange={(e) => handleBrandChange(e.target.value)}>
                            <option value="default" key="default">Select brand</option>
                            {brands.map((brand) => <option value={brand.brandId} key={brand.brandId}>{brand.name}</option>)}
                        </Form.Select> : <p value="default">There are no available brands.</p>}
                </Form.Group>
            }
            {productTypes &&
                <Form.Group className="mb-3" controlId="formBasicProductType" key="formBasicProductType" >
                    <Form.Label>Product Type</Form.Label>
                    {productTypes.length > 0 ?
                        <Form.Select aria-label="Default select example" onChange={(e) => handleProductTypeChange(e.target.value)}>
                            <option value="default" key="default">Select product type</option>
                            {productTypes.map((prodType) => <option value={prodType.productTypeId} key={prodType.productTypeId}>{prodType.name}</option>)}
                        </Form.Select> : <p value="default">There are no available product types.</p>}
                </Form.Group>
            }
            {productType && productType.attributes.length > 0 &&
                <div>
                    {productType.attributes.map((field, index) => (
                        <Form.Group className="mb-3" controlId={field.attributeId} key={field.attributeId}>
                            <Form.Label>{field.attributeName}</Form.Label>
                            <Form.Control type="text" placeholder={`Enter value for ${field.attributeName}`} value={attributes[field.attributeId]} onChange={(e) => handleChange(field.attributeId, e.target.value)} required />
                        </Form.Group>
                    ))}
                </div>
            }
            <Button variant="dark" onClick={handleCancel}>Cancel</Button>
            <Button variant="outline-dark" onClick={handleSave} disabled={!name || !brand || !category || (!productType || attributes.length < productType.attributes.length)}>Save</Button>
        </Form>
    );
}

export default ProductForm;