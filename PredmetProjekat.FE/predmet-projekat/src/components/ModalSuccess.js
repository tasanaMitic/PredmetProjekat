import {Modal, Button} from 'react-bootstrap';
import PropTypes from 'prop-types';

const ModalSuccess = ({show, setShow, clearData, message}) => {
    const handleClose = () => setShow(false);
    const handleOk = () =>{
        setShow(false);
        clearData();
    }

    return (
        <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Success!</Modal.Title>
        </Modal.Header>
        <Modal.Body>{message}</Modal.Body>
        <Modal.Footer>
          <Button variant="outline-dark" onClick={handleOk}>
            OK
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }

  ModalSuccess.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    clearData: PropTypes.func,
    message: PropTypes.string
  }
  
  export default ModalSuccess;