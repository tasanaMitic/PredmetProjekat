import {Modal, Button} from 'react-bootstrap'

function ModalSuccess({show, setShow, clearData, message}) {
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
  
  export default ModalSuccess;