import React, { Component } from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

export default function Layout(props) {
    console.log(props);
    return (
      <div>
        <NavMenu />
          {props.children}
      </div>
    );
 }

