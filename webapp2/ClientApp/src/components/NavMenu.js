import React, { Component, useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom'
import './NavMenu.css';
import axios from "axios";
import { useHistory } from "react-router-dom"

export default function NavMenu(props) {
    console.log(props);
    const [toggleNavbar, setToggleNavbar] = useState();
    const history = useHistory()

    const handleToggleNavbar = () => {
        setToggleNavbar(!toggleNavbar);
    }

    const loggUt = async () => {
        await axios.get('Kunde/LoggUt')
            .then(function (response) {
                // handle success
                console.log(response)
                if (response.status == 200) {
                    props.falseSession();
                    history.push("/")
                }
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            })
            .then(function () {
                // always executed
            });
    }
 

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow m-0" light>
          <Container>
            <NavbarBrand tag={Link} to="/">webapp2</NavbarBrand>
            <NavbarToggler onClick={handleToggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!toggleNavbar} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>

                <NavItem>
                   <NavLink tag={Link} className="text-dark" to="/admin">Admin</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/logginn">Logg inn</NavLink>
                </NavItem>
                {props.validSession === true &&
                <NavItem>
                    <NavLink tag={Link} className="text-dark" onClick={loggUt}>Logg ut</NavLink>
                </NavItem>
                }
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    )
} 