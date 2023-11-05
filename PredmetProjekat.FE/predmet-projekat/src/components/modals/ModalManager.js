import { Modal, Button, Container } from 'react-bootstrap';
import { useState } from 'react';
import ManagersTable from '../ManagersTable';
import PropTypes from 'prop-types';

const ModalManager = ({ show, setShow, managers, confirm, loggedInUser }) => { //todo  if managers == null show message
  const [selectedManager, setSelectedManager] = useState(null);
  const handleClose = () => {
    setShow(false);
    setSelectedManager(null);
  }
  const handleOk = () => {
    setShow(false);
    setSelectedManager(null);
  }
  const handleConfirm = () => {
    setShow(false);
    setSelectedManager(null);
    confirm(selectedManager);
  }

  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Please select one manager</Modal.Title>
      </Modal.Header>
      {managers && managers.length > 0 ?
        <Container>
          <Modal.Body>
            <ManagersTable managers={managers} loggedInUser={loggedInUser} setSelectedManager={setSelectedManager} selectedManager={selectedManager} />
          </Modal.Body>
          <Modal.Footer>
            <Button variant="outline-dark" onClick={handleConfirm} disabled={!selectedManager}>
              Confirm
            </Button>
          </Modal.Footer>
        </Container>
        :
        <Container>
          <Modal.Body>
            <p>There are not available managers.</p>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="outline-dark" onClick={handleOk}>
              Ok
            </Button>
          </Modal.Footer>
        </Container>
      }
    </Modal>
  );
}

ModalManager.propTypes = {
  show: PropTypes.bool,
  setShow: PropTypes.func,
  confirm: PropTypes.func,
  selectedUser: PropTypes.string,
  employees: PropTypes.arrayOf(PropTypes.shape({
    email: PropTypes.string,
    firstName: PropTypes.string,
    lastName: PropTypes.string,
    username: PropTypes.string,
    managerId: PropTypes.string,
    manager: PropTypes.object   //todo
  })),
  loggedInUser: PropTypes.shape({
    role: PropTypes.string,
    username: PropTypes.string
  })
}

export default ModalManager;