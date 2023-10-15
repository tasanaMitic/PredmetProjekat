import { Modal, Button } from 'react-bootstrap';
import PropTypes from 'prop-types';

const ModalCheck = ({show, setShow, confirm}) => {   //to do message
    const handleClose = () => setShow(false);
    const handleOk = () =>{
        setShow(false);
        confirm();
    }

    return (
        <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Are you sure?</Modal.Title>
        </Modal.Header>
        <Modal.Body>Are you sure you want to do this?</Modal.Body>  
        <Modal.Footer>
          <Button variant="outline-dark" onClick={handleOk}>
            OK
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }

  ModalCheck.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    confirm: PropTypes.func
  }
  
  export default ModalCheck;