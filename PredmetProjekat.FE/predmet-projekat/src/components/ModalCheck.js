import {Modal, Button} from 'react-bootstrap'

function ModalCheck({show, setShow, confirm}) {   //to do message
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
          <Button variant="primary" onClick={handleOk}>
            OK
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }
  
  export default ModalCheck;