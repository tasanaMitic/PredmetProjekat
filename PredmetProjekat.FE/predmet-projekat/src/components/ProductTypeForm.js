import { useState } from "react";
import { Form, Button, Container } from "react-bootstrap";
import { createProductType } from "../api/methods";
import { useHistory } from "react-router-dom";
import ModalError from "./modals/ModalError";
import ModalSuccess from "./modals/ModalSuccess";

const ProductTypeForm = () => {
    const history = useHistory();
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);
    const [successModal, setSuccessModal] = useState(false);

    const [name, setName] = useState('');
    const [formFields, setFormFields] = useState(['', '', '']);

    const handleAddClick = () => {
        setFormFields([...formFields, '']);
    };

    const handleRemoveClick = () => {
        const updatedFields = [...formFields];
        updatedFields.pop();
        setFormFields(updatedFields);
    };

    const handleChange = (index, value) => {
        const updatedFields = [...formFields];
        updatedFields[index] = value;
        setFormFields(updatedFields);
    };

    const handleSave = (e) => {
        e.preventDefault();

        const payload = {
            name: name,
            attributes: formFields
        };

        createProductType(payload).then(res => {
            if (res.status !== 201) {
                throw Error('There was an error with the request!');
            }
            setSuccessModal(true);
            setSuccessMessage("You have successfully created a product type with name " + name);
            setError(null);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
        })

    };

    const handleCancel = () => {
        history.replace('/products');
    }

    return (
        <Container className="d-flex justify-content-center">
            <Form>
                <h1>Create new Product Type</h1>
                <ModalSuccess setShow={setSuccessModal} show={successModal} clearData={handleCancel} message={successMessage} />
                {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
                <Form.Group className="custom-width mb-3" controlId="formBasicName" key="formBasicName" >
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter product type name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required />
                </Form.Group>
                <h5>Please enter at least 3 attributes</h5>
                {formFields.map((field, index) => (
                    <Form.Group className="custom-width mb-3" key={"formBasic" + index} >
                        <Form.Label>{`Attribute ${index + 1}:`}</Form.Label>
                        <Form.Control
                            type="text"
                            value={field}
                            placeholder="Enter attribute name"
                            onChange={(e) => handleChange(index, e.target.value)}
                        />
                    </Form.Group>
                ))}
                <Container className="d-flex justify-content-center">
                    <Button  onClick={handleAddClick}>Add attribute</Button>
                    {formFields.length >= 4 && (
                        <Button onClick={handleRemoveClick}>Remove last added attribute</Button>
                    )}
                </Container>
                <Container className="d-flex justify-content-center">
                    <Button variant="dark" style={{ marginRight: '10px' }} onClick={handleCancel}>Cancel</Button>
                    <Button variant="outline-dark" style={{ marginRight: '10px' }} onClick={handleSave} disabled={!name || formFields.some(field => field.trim() === '')}>Save</Button>
                </Container>
            </Form>
        </Container>
    );
}

export default ProductTypeForm;