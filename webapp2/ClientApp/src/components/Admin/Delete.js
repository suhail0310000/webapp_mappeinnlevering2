import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Button, Modal } from 'react-bootstrap';
import { AiFillDelete } from 'react-icons/ai';
import Alert from 'react-bootstrap/Alert'

export default function Delete(props) {
    const [show, setShow] = useState(false);
    const [noPossible, setNoPossible] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const deleteReise = async () => {
        console.log(props.id);
        let id = props.id;
        await axios.delete('Kunde/SlettReise/' + props.id)
            .then(function (response) {
                // handle success
                console.log(response);
                if (response.data) {
                    props.deleteReise(id);
                    handleClose();
                } 
            })
            .catch(function (error) {
                // handle error
                setNoPossible(true);
                console.log(error);
            })
            .then(function () {
                // always executed
            });
    }
    return (
        <div>
            <Button variant="danger" onClick={handleShow}>
                <AiFillDelete/>
            </Button>
            <Modal animation={false} show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Slett reise</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <h4>Er du sikker på at du vil slette reisen?</h4>
                    {noPossible &&
                        <Alert variant="danger" transition={false}>
                            <Alert.Heading>Du kan ikke slette reise!</Alert.Heading>
                            <p>
                                Det finnes en ordre på denne reisen
                            </p>
                        </Alert>
                    }
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Nei
                    </Button>
                    <Button variant="primary" onClick={deleteReise}>
                        Ja
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}