import { Button, Container, Table, InputGroup, Form, FormControl } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useHistory } from "react-router-dom";
import { useEffect, useState } from "react";
import { getStockedProducts } from "../../api/methods";
import ModalSell from "../modals/ModalSellProduct";
import ModalError from "../modals/ModalError";
import ModalSuccess from "../modals/ModalSuccess";

const SellProductsPage = ({ user }) => {
    const history = useHistory();
    const [data, setData] = useState(null);
    const [filterdData, setFilteredData] = useState(null);
    const [selectedProductIds, setSelectedProductIds] = useState([]);
    const [selectedProducts, setSelectedProducts] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');

    const [sellModal, setSellModal] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(null);
    const [successMessage, setSuccessMessage] = useState(null);
    const [successModal, setSuccessModal] = useState(false);

    useEffect(() => {
        getStockedProducts().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        }).then((data) => {
            setData(data);
            setFilteredData(data);
        }).catch(err => {
            setData(null);
        });

    }, []);

    const handleChange = (e, productId) => {
        e.target.checked ?
            setSelectedProductIds((prevSelectedProductIds) => [...prevSelectedProductIds, productId])
            : setSelectedProductIds((prevSelectedProductIds) =>
                prevSelectedProductIds.filter((id) => id !== productId));
    };

    const handleSubmit = () => {
        setSellModal(true);
        setSelectedProducts(data.filter(product => {
            return selectedProductIds.includes(product.productId)
        }));
    }

    const handleCancel = () => {
        history.replace('/products');
    }

    const handleSearch = (value) => {
        setSearchTerm(value);
        value === '' ? 
            setFilteredData(data) : 
            setFilteredData(data.filter(x => x.name.includes(value)));
    };

    return (
        <Container className="d-flex flex-column align-items-center p-3">
            <ModalSuccess setShow={setSuccessModal} show={successModal} clearData={handleCancel} message={successMessage} />
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
            {sellModal && <ModalSell setShow={setSellModal}
                show={sellModal}
                setError={setError}
                setErrorModal={setErrorModal}
                selectedProducts={selectedProducts}
                setSuccessMessage={setSuccessMessage}
                setSuccessModal={setSuccessModal} />}
            <h1>Select products to sell</h1>
            <Container>
                <Form>
                    <FormControl
                        type="text"
                        placeholder="Search"
                        className="mb-2 mt-2"
                        value={searchTerm}
                        onChange={(e) => handleSearch(e.target.value)}
                    />
                </Form>
            </Container>
            {filterdData && filterdData.length > 0 ?
                <Container>
                    <Table striped hover>
                        <thead>
                            <tr>
                                <th>Select</th>
                                <th>Product name</th>
                                <th>Product type</th>
                                <th>Price</th>
                                <th>Category</th>
                                <th>Brand</th>
                            </tr>
                        </thead>
                        <tbody>
                            {filterdData.map((product) => (
                                <tr key={product.productId}>
                                    <td>
                                        <InputGroup onChange={(e) => handleChange(e, product.productId)}>
                                            <InputGroup.Checkbox aria-label="Checkbox for following text input" />
                                        </InputGroup>
                                    </td>
                                    <td>{product.name}</td>
                                    <td>{product.productType.name}</td>
                                    <td>{product.price}$</td>
                                    <td>{product.category.name}</td>
                                    <td>{product.brand.name}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </Container>
                :
                <h3>There are no available products!</h3>
            }
            <Button variant="outline-dark" onClick={handleSubmit} disabled={selectedProductIds.length < 1}>Submit</Button>
        </Container>

    );
}

SellProductsPage.propTypes = {
    user: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}

export default SellProductsPage;