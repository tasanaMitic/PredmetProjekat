import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

const AppHeader = ({ user, logout }) => {
  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand as={Link} to="/">The Ultimate Store</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          {user ? (
            user.role === 'Admin' ?
              <Nav className="me-auto">
              <NavDropdown title="Users" id="basic-nav-dropdown">
                  <NavDropdown.Item as={Link} to="/register">Register</NavDropdown.Item>
                  <NavDropdown.Divider />                  
                  <NavDropdown.Item as={Link} to="/admins">Admins</NavDropdown.Item>
                  <NavDropdown.Item as={Link} to="/employees">Employees</NavDropdown.Item>
                </NavDropdown>
                <Nav.Link as={Link} to="/products">Products</Nav.Link>               
                <Nav.Link as={Link} to="/brands&categories">Brands&Categories</Nav.Link>
                <Nav.Link as={Link} to="/registers">Registers</Nav.Link>
                <NavDropdown title={user.username} id="basic-nav-dropdown">
                  <NavDropdown.Item as={Link} to="/account">My account</NavDropdown.Item>
                  <NavDropdown.Item as={Link} to="/sales">Finances</NavDropdown.Item>
                  <NavDropdown.Divider />                  
                  <NavDropdown.Item onClick={logout} >Logout</NavDropdown.Item>
                </NavDropdown>
              </Nav>
              :
              <Nav className="me-auto">
                <Nav.Link as={Link} to="/products">Products</Nav.Link>
                <Nav.Link as={Link} to="/managers">Managers</Nav.Link>
                <Nav.Link as={Link} to="/sell">Sell</Nav.Link>
                <NavDropdown title={user.username} id="basic-nav-dropdown">
                  <NavDropdown.Item as={Link} to="/account">My account</NavDropdown.Item>
                  <NavDropdown.Item as={Link} to="/sales">History of sales</NavDropdown.Item>
                  <NavDropdown.Divider />                  
                  <NavDropdown.Item onClick={logout} >Logout</NavDropdown.Item>
                </NavDropdown>
              </Nav>

          ) : (
            <Nav className="me-auto">
              <Nav.Link as={Link} to="/login">Login</Nav.Link>
            </Nav>
          )}

        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}

AppHeader.propTypes = {
  user: PropTypes.shape({
    role: PropTypes.string,
    username: PropTypes.string
  }),
  logout: PropTypes.func
}

export default AppHeader;