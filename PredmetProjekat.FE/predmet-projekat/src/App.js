import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from "react";
import Cookies from 'universal-cookie';
import jwt from "jwt-decode";
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import AppHeader from './components/Header';
import ProductsPage from './components/pages/ProductsPage';
import HomePage from './components/pages/HomePage';
import AccountPage from './components/pages/AccountPage'
import EmployeesPage from './components/pages/EmployeesPage';
import BrandsAndCetegoriesPage from './components/pages/BrandsAndCategoriesPage';
import AdminsPage from './components/pages/AdminsPage';
import LoginPage from './components/pages/LoginPage';
import RegisterPage from './components/pages/RegisterPage';
import SellProductsPage from './components/pages/SellProductsPage';
import ManagersPage from './components/pages/ManagersPage';
import RegistersPage from './components/pages/RegistersPage';
import SalesPage from './components/pages/SalesPage';
import ProductForm from './components/ProductForm';
import ProductTypeForm from './components/ProductTypeForm';


const App = () => {
  const cookies = new Cookies();
  const [user, setUser] = useState(null);

  const logout = () => {
    setUser(null);
    cookies.remove("jwt_authorization");
  };

  const login = (jwtToken) => {
    const decoded = jwt(jwtToken);
    setUser({role : decoded.role, username : decoded.username});

    cookies.set("jwt_authorization", jwtToken, {
      expires: new Date(decoded.exp * 1000),
    })
  };

  return (
    <Router>
      <div className="App">
        <header id="header">
          <AppHeader user={user} logout={logout}></AppHeader>
        </header>
        <main id="main">        
        { user ? (
          <Switch>
          <Route exact path="/">
            <HomePage user={user}></HomePage>
          </Route>
          <Route exact path="/register">
            <RegisterPage></RegisterPage>
          </Route>
          <Route exact path="/products">
            <ProductsPage user={user}></ProductsPage>
          </Route>
          <Route exact path="/brands&categories">
            <BrandsAndCetegoriesPage></BrandsAndCetegoriesPage>
          </Route>
          <Route exact path="/account">
            <AccountPage user={user}></AccountPage>
          </Route>   
          <Route exact path="/employees">
            <EmployeesPage user={user}></EmployeesPage>
          </Route>
          <Route exact path="/admins">
            <AdminsPage user={user}></AdminsPage>
          </Route>
          <Route exact path="/managers">
            <ManagersPage user={user}></ManagersPage>
          </Route>
          <Route exact path="/addproduct">
            <ProductForm></ProductForm>
          </Route>
          <Route exact path="/addproducttype">
          <ProductTypeForm></ProductTypeForm>
        </Route>
          <Route exact path="/sell">
            <SellProductsPage user={user}></SellProductsPage>
          </Route>   
          <Route exact path="/sales">
            <SalesPage user={user}></SalesPage>
          </Route>        
          <Route exact path="/registers">
            <RegistersPage></RegistersPage>
          </Route>
        </Switch>
        ) : (
          <Switch>
          <Route exact path="/">
            <HomePage user={user}></HomePage>
          </Route>
          <Route exact path="/login">
            <LoginPage loginUser={login}></LoginPage>
          </Route>
        </Switch>
        )}          
        </main>
      </div>
    </Router>
  );
}

export default App;
