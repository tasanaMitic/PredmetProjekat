import { Modal, Button } from 'react-bootstrap';
import PropTypes from 'prop-types';

const ModalError = ({ show, setShow, error, setError }) => {

    const handleClick = () => {
        setShow(false);
        setError(null);
    }

    const renderSwitch = (param) => {
        console.log(error);
        switch (param) {
            case 400:
                return error.data.Message ? error.data.Message : "Bad requst was sent to the server. Please check your inputs.";
            case 401:
                return "You are not authorized to perform this request."
            case 404:
                return error.data.message;
            case 500:
                return error.data.message;
            default:
                return "Something went wrong."
        }
    }

    return (
        <Modal show={show} onHide={handleClick}>
            <Modal.Header closeButton>
                <Modal.Title>Oh snap!</Modal.Title>
            </Modal.Header>
            <Modal.Body>{error.data ? renderSwitch(error.status) : "Something went wrong! Try again shortly!"}</Modal.Body>
            <Modal.Footer>
                <Button variant="outline-dark" onClick={handleClick}>
                    OK
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

ModalError.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    setError: PropTypes.func,
    error: PropTypes.shape({
        status: PropTypes.number,
        data: PropTypes.shape({
            Message: PropTypes.string
        })
    })
}

export default ModalError;