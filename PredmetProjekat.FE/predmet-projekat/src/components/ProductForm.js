import { useEffect, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { getBrands, getCategories, createProduct } from "../api/methods";
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

    const [name, setName] = useState('');
    const [size, setSize] = useState(null);
    const [sex, setSex] = useState(null);
    const [season, setSeason] = useState(null);
    const [brand, setBrand] = useState(null);
    const [category, setCategory] = useState(null);

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


    }, [])

    const handleBrandChange = (brandId) => {
        setBrand(brandId === 'default' ? null : brandId);
    }
    
    const handleCategoryChange = (categoryId) => {
        setCategory(categoryId === 'default' ? null : categoryId);
    }
    
    const handleSeasonChange = (season) => {
        setSeason(season === 'default' ? null : season);
    }
    
    const handleSizeChange = (size) => {
        setSize(size === 'default' ? null : size);
    }
    
    const handleSexChange = (sex) => {
        setSex(sex === 'default' ? null : sex);
    }

    const handleSave = () => {
        const payload = {
            name: name,
            size: size,
            sex: sex,
            season: season,
            categoryId: category,
            brandId: brand
        };

        createProduct(payload).then(res => {
            if (res.status !== 201) {
                throw Error('There was an error with the request!');
            }
            setSuccessModal(true);
            setSuccessMessage("You have successfully creates a product with name " + name);
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
            <Form.Group className="mb-3" controlId="formBasicSize" key="formBasicSize" >
                <Form.Label>Size</Form.Label>
                <Form.Select aria-label="Default select example" onChange={(e) => handleSizeChange(e.target.value)}>
                    <option value="default" key="default">Select size</option>
                    <option value="xs" key="xs">XS</option>
                    <option value="s" key="s">S</option>
                    <option value="m" key="m">M</option>
                    <option value="l" key="l">L</option>
                    <option value="xl" key="xl">XL</option>
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicSex" key="formBasicSex" >
                <Form.Label>Sex</Form.Label>
                <Form.Select aria-label="Default select example" onChange={(e) => handleSexChange(e.target.value)}>
                    <option value="default" key="default">Select sex</option>
                    <option value="m" key="m">Male</option>
                    <option value="f" key="f">Female</option>
                    <option value="k" key="k">Kids</option>
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicSeason" key="formBasicSeason" >
                <Form.Label>Season</Form.Label>
                <Form.Select aria-label="Default select example" onChange={(e) => handleSeasonChange(e.target.value)}>
                    <option value="default" key="default">Select season</option>
                    <option value="summer" key="summer">Summer</option>
                    <option value="fall" key="fall">Fall</option>
                    <option value="winter" key="winter">Winter</option>
                    <option value="spring" key="spring">Spring</option>
                </Form.Select>
            </Form.Group>
            {categories &&
                <Form.Group className="mb-3" controlId="formBasicCategory" key="formBasicCategory" >
                    <Form.Label>Category</Form.Label>
                    {categories.length > 0 ? 
                    <Form.Select aria-label="Default select example" onChange={(e) => handleCategoryChange(e.target.value)}>
                        <option value="default" key="default">Select category</option>
                        {categories.map((category) => <option value={category.categoryId} key={category.categoryId}>{category.name}</option>)}
                    </Form.Select> : <p value="default">There are no available categories.</p>}
                </Form.Group>}
            {brands &&
                <Form.Group className="mb-3" controlId="formBasicBrand" key="formBasicBrand" >
                    <Form.Label>Brand</Form.Label>
                    {brands.length > 0 ?
                        <Form.Select aria-label="Default select example" onChange={(e) => handleBrandChange(e.target.value)}>
                            <option value="default" key="default">Select brand</option>
                            {brands.map((brand) => <option value={brand.brandId} key={brand.brandId}>{brand.name}</option>)}
                        </Form.Select> : <p value="default">There are no available brands.</p>}
                </Form.Group>}
            <Button variant="dark" onClick={handleCancel}>Cancel</Button>
            <Button variant="outline-dark" onClick={handleSave} disabled={!brand || !category || !name || !size || !season || !sex}>Save</Button>
        </Form>
    );
}

export default ProductForm;