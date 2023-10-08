import {Modal, Button} from 'react-bootstrap'
import { useState } from 'react';
import ManagersTable from './ManagersTable';

function ModalManager({show, setShow, managers, confirm, loggedInUser, selectedUser}) { 
    const [selectedManager, setSelectedManager] = useState(null);
    const handleClose = () => {
        setShow(false);
        setSelectedManager(null);
    }
    const handleOk = () =>{
        setShow(false);
        setSelectedManager(null);
        confirm(selectedManager);
    }

    return (
        <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Please select one manager</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <ManagersTable managers={managers} loggedInUser={loggedInUser} setSelectedManager={setSelectedManager} selectedManager={selectedManager} selectedUser={selectedUser} />
        </Modal.Body>  
        <Modal.Footer>
          <Button variant="outline-dark" onClick={handleOk} disabled={!selectedManager}>
            Confirm
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }
  
  export default ModalManager;