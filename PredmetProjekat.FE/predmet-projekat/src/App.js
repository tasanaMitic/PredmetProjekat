import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from "react";
import Cookies from 'universal-cookie';
import jwt from "jwt-decode";

import AppHeader from './components/Header';
import ProductsPage from './components/pages/ProductsPage';
import HomePage from './components/pages/HomePage';
import AccountPage from './components/pages/AccountPage'
import OrderPage from './components/pages/OrderPage';
import EmployeesPage from './components/pages/EmployeesPage';
import BrandsAndCetegoriesPage from './components/pages/BrandsAndCategoriesPage';
import AdminsPage from './components/pages/AdminsPage';
import LoginPage from './components/pages/LoginPage';
import RegisterPage from './components/pages/RegisterPage';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

function App() {
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

  useEffect(() => {
    console.log(user);

  },[user]);

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
            <HomePage></HomePage>
          </Route>
          <Route exact path="/register">
            <RegisterPage></RegisterPage>
          </Route>
          <Route path="/products">
            <ProductsPage></ProductsPage>
          </Route>
          <Route path="/brands&categories">
            <BrandsAndCetegoriesPage></BrandsAndCetegoriesPage>
          </Route>
          <Route path="/account">
            <AccountPage user={user}></AccountPage>
          </Route>
          <Route path="/order">
            <OrderPage></OrderPage>
          </Route>       
          <Route path="/employees">
            <EmployeesPage user={user}></EmployeesPage>
          </Route>
          <Route path="/admins">
            <AdminsPage user={user}></AdminsPage>
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
