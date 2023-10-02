import {Modal, Button} from 'react-bootstrap'

function StaticExample({show, setShow, clearData}) {
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
        <Modal.Body>Woohoo, you are reading this text in a modal!</Modal.Body>
        <Modal.Footer>
          <Button variant="primary" onClick={handleOk}>
            OK
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }
  
  export default StaticExample;