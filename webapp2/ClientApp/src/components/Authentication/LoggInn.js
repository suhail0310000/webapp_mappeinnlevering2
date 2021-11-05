import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Redirect } from 'react-router-dom'

export default function LoggInn(props) {
    const [userName, setUserName] = useState('')
    const [passord, setPassord] = useState('')
    const [redirectToReferrer, setRedirectToReferrer] = useState(false)
    console.log(props);

    const updateForm = () => {
        setUserName('')
        setPassord('')
    }

    const handleSubmit = async (e) => {
        console.log("submitted")
        e.preventDefault()

        const bruker = {
            brukernavn: userName,
            passord: passord
        }

        console.log(bruker)
        updateForm()

        await axios.post('Kunde/LoggInn', bruker)
            .then(function (response) {
                // handle success
                console.log(response);
                if(response.data === true) {
                    setRedirectToReferrer(true);
                    props.trueSession();
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
    
    const { from } = props.info.location.state || {
        from: {
            pathname: '/admin'
        }
    }

    if (redirectToReferrer) {
        return (<Redirect to={from} />)
    }
    

    return (
        <div className="container">
            <h1>Logg inn</h1>
            <form className="form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Brukernavn</label>
                    <input type="text" value={userName} onChange={(e) => setUserName(e.target.value)} />
                </div>
                <div className="form-group">
                    <label>Passord</label>
                    <input type="password" value={passord} onChange={(e) => setPassord(e.target.value)} />
                </div>
                <div className="form-group">
                    <button>Logg inn</button>
                </div>

                <p>brukernavn - {userName}</p>
                <p>passord - {passord} </p>
                <p onClick={updateForm}> update form </p>
                <div></div>
            </form>
        </div>

    );

}
