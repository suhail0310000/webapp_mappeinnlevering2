import React, { Component, useEffect, useState } from 'react';
import { Route } from 'react-router';
import Home from './components/Home/MultiStep';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import Admin from './components/Admin/Admin';
import './custom.css'
import Logginn from './components/Authentication/LoggInn';
import axios from "axios";
import NavMenu from './components/NavMenu';

export default function App() {
    const [validSession, setValidSession] = useState(false);

    useEffect(() => {
        async function checkSession() {
            await axios.get('Kunde/ValidSession')
                .then(function (response) {
                    // handle success
                    console.log(response)
                    if (response.data === true) {
                        setValidSession(true);
                    } else {
                        setValidSession(false);
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
        checkSession()
    }, [])

    const trueSession = () => {
        setValidSession(true);
    }

    const falseSession = () => {
        setValidSession(false);
    }

 
    return (
        <>
            <NavMenu validSession={validSession} falseSession={falseSession}/>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route path='/admin' component={Admin} />
            <Route path='/logginn' component={(info) => (<Logginn info={info} trueSession={trueSession} />)} />
        </>
   );
}
