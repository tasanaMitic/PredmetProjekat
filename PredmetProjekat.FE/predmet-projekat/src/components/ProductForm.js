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
        brandId === ('default') ? setBrand(null) : setBrand(brandId);
    }

    const handleCategoryChange = (categoryId) => {
        categoryId === ('default') ? setCategory(null) : setCategory(categoryId);
    }

    const handleSeasonChange = (season) => {
        season === ('default') ? setSeason(null) : setSeason(season);
    }

    const handleSizeChange = (size) => {
        size === ('default') ? setSize(null) : setSize(size);
    }

    const handleSexChange = (sex) => {
        sex === ('default') ? setSex(null) : setSex(sex);
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
                    <option value="default">Select size</option>
                    <option value="xs">XS</option>
                    <option value="xs">S</option>
                    <option value="xs">M</option>
                    <option value="xs">L</option>
                    <option value="xs">XL</option>
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicSex" key="formBasicSex" >
                <Form.Label>Sex</Form.Label>
                <Form.Select aria-label="Default select example" onChange={(e) => handleSexChange(e.target.value)}>
                    <option value="default">Select sex</option>
                    <option value="m">Male</option>
                    <option value="f">Female</option>
                    <option value="k">Kids</option>
                </Form.Select>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicSeason" key="formBasicSeason" >
                <Form.Label>Season</Form.Label>
                <Form.Select aria-label="Default select example" onChange={(e) => handleSeasonChange(e.target.value)}>
                    <option value="default">Select season</option>
                    <option value="summer">Summer</option>
                    <option value="fall">Fall</option>
                    <option value="winter">Winter</option>
                    <option value="spring">Spring</option>
                </Form.Select>
            </Form.Group>
            {categories &&
                <Form.Group className="mb-3" controlId="formBasicCategory" key="formBasicCategory" >
                    <Form.Label>Category</Form.Label>
                    {categories.length > 0 ? 
                    <Form.Select aria-label="Default select example" onChange={(e) => handleCategoryChange(e.target.value)}>
                        <option value="default">Select category</option>
                        {categories.map((category) => <option value={category.categoryId}>{category.name}</option>)}
                    </Form.Select> : <p value="default">There are no available categories.</p>}
                </Form.Group>}
            {brands &&
                <Form.Group className="mb-3" controlId="formBasicBrand" key="formBasicBrand" >
                    <Form.Label>Brand</Form.Label>
                    {brands.length > 0 ?
                        <Form.Select aria-label="Default select example" onChange={(e) => handleBrandChange(e.target.value)}>
                            <option value="default">Select brand</option>
                            {brands.map((brand) => <option value={brand.brandId}>{brand.name}</option>)}
                        </Form.Select> : <p value="default">There are no available brands.</p>}
                </Form.Group>}
            <Button variant="outline-dark" onClick={handleCancel}>Cancel</Button>
            <Button variant="outline-dark" onClick={handleSave} disabled={!brand || !category || !name || !size || !season || !sex}>Save</Button>
        </Form>
    );
}

export default ProductForm;