import React, { useState, useEffect } from 'react';
import './home.css';
import axios from "axios";

export default function Home() {
    const [steder, setSteder] = useState([]);
    const [destinasjoner, setDestinasjoner] = useState([]);

    useEffect(() => {
        async function fetchSteder() {
            console.log('Fetch...');
            await axios.get('Kunde/GetAllSteder')
                .then(function (response) {
                    // handle success
                    console.log(response);
                    setSteder(response.data);
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .then(function () {
                    // always executed
                });
        }
        fetchSteder();
    },[])
  
    const populateDestinasjoner = async (e) => {
        console.log(e.target.value);
        let avgang = e.target.value;

        const mulige_destinasjoner = await axios.get('Kunde/GetAllDestinasjoner?avgang=' + avgang)
        .then(function (response) {
            console.log(response);
            return response.data;
        })
        .catch(function (error) {
            console.log(error);
        });

        console.log(mulige_destinasjoner);
        setDestinasjoner(mulige_destinasjoner);
    }

    return (
        <div id="booking" className="section">
            <div className="section-center">
                <div className="container">
                    <div className="row">
                        <div className="col-md-7 col-md-push-5">
                            <div className="booking-cta">
                                <h1>Make your reservation</h1>
                                <p>
                                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Animi facere, soluta magnam consectetur molestias itaque
                                    ad sint fugit architecto incidunt iste culpa perspiciatis possimus voluptates aliquid consequuntur cumque quasi.
                                    Perspiciatis.
                                </p>
                            </div>
                        </div>
                        <div className="col-md-4 col-md-pull-7">
                            <div className="booking-form">
                                <div id="alertBox"></div>
                                {/*
                                <div className="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div className="modal-dialog" role="document">
                                        <div className="modal-content">
                                            <div className="modal-header">

                                                <h4 className="modal-title" id="exampleModalLabel">Billett funnet etter dine kvalifikasjoner:</h4>
                                                <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div className="modal-body">
                                                <div className="row">
                                                    <div className="col-md-12">
                                                        <div className="panel panel-default">
                                                            <div className="panel-heading" style={{ backgroundColor: "gold" }}>
                                                                <h3 className="panel-title"><strong>Reisedetaljer</strong></h3>
                                                            </div>

                                                            <div className="panel-body">
                                                                <div className="table-responsive">
                                                                    <table className="table table-condensed">
                                                                        <thead>
                                                                            <tr>
                                                                                <td><strong>Avgang</strong></td>
                                                                                <td class="text-center"><strong>Destinasjon</strong></td>
                                                                                <td class="text-center"><strong>Dato</strong></td>
                                                                                <td class="text-center"><strong>Tid</strong></td>
                                                                                <td class="text-center"><strong>Barne biletter</strong></td>
                                                                                <td class="text-center"><strong>Student biletter</strong></td>
                                                                                <td class="text-center"><strong>Voksen biletter</strong></td>
                                                                                <td class="text-right"><strong>Total pris:</strong></td>
                                                                                <td><strong>Fra sted:</strong></td>
                                                                                <td class="text-center"><strong>Til sted:</strong></td>
                                                                                <td class="text-center"><strong>Tid:</strong></td>
                                                                                <td class="text-right"><strong>Total pris:</strong></td>-->
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="bilettTabell">
                                                                            <tr key={"hello"}>
                                                                                <td>BS-200</td>
                                                                                <td class="text-center">$10.99</td>
                                                                                <td class="text-center">1</td>
                                                                                <td class="text-center">1</td>
                                                                                <td class="text-center">1</td>
                                                                                <td class="text-center">$10.99</td>
                                                                                <td class="text-center">1</td>
                                                                                <td class="text-right">$10.99</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div className="container mt-5 mb-5 d-flex justify-content-center">
                                                                <div className="card px-1 py-4">
                                                                    <div className="card-body">
                                                                        <h5 style={{ color: "red" }}>Fyll ut kontaktinformasjon for å fullføre bestillingen</h5>
                                                                        <div className="row">
                                                                            <div className="col-sm-6">
                                                                                <div className="form-group">
                                                                                    <label htmlFor="name">Name</label><input class="form-control" type="text" placeholder="Fornavn" id="inpFornavn" />
                                                                                </div>

                                                                            </div>
                                                                            <div className="col-sm-6">
                                                                                <div className="form-group">
                                                                                    <label htmlFor="name">Name</label>  <input class="form-control" type="text" placeholder="Etternavn" id="inpEtternavn" />
                                                                                </div>
                                                                            </div>

                                                                            <div className=" d-flex flex-column text-center px-5 mt-3 mb-3"> <small class="agree-text">By Booking this appointment you agree to the</small> <a href="#" class="terms">Terms & Conditions</a> </div> <button class="btn btn-primary btn-block confirm-button">Confirm</button>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div className="modal-footer">
                                                    <button type="button" className="btn btn-secondary" data-dismiss="modal">Lukk</button>
                                                    <button type="button" type="submit" className="btn btn-primary" onclick="fullførOrdre()">Fullfør bestilling</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                */}
                        
                                    <form>
                                        <div className="form-group">
                                        <span className="form-label">Avgang</span>
                                            {steder.length > 0 &&
                                                <select onChange={populateDestinasjoner}>
                                                    {steder.map((sted, index) => 
                                                        <option>{sted.stedsNavn}</option>
                                                    )}
                                                </select>
                                            }
                                            <div id="selectAvgang"></div>
                                        </div>

                                        <div className="form-group">
                                            <span className="form-label">Destinasjon</span>
                                            {destinasjoner.length > 0 &&
                                                <select>
                                                    {destinasjoner.map((destinasjon, index) =>
                                                        <option>{destinasjon.stedsNavn}</option>
                                                    )}
                                                </select>
                                            }
                                            <div id="feilDestinasjon" style={{ color: "red" }}></div>
                                        </div>


                                        <div className="form-group">
                                            <span className="form-label">Avreise dato:</span>
                                            <div id="selectDato"></div>
                                            <div id="feilDato" style={{ color: "red" }}></div>
                                        </div>

                                        <div className="form-group">
                                            <span className="form-label">Avreise tid:</span>
                                            <div id="selectTid"></div>
                                            <div id="feilTid" style={{ color: "red" }}></div>
                                        </div>

                                        <div className="row">
                                            <div className="col-sm-6">
                                                <div className="form-group">
                                                    <span className="form-label">Barn</span>
                                                    <div className="form-control-sm-6">
                                                        <input value="0" type="number" id="antBarnBiletter"/>
                                                        <div id="feilAntallBarn" style={{ color: "red" }}></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="col-sm-6">
                                                <div className="form-group">
                                                    <span className="form-label">Student</span>

                                                    <div className="form-control-sm-6">
                                                        <input value="0" type="number" id="antStudentBiletter"/>
                                                        <div id="antallStudenterFeil" style={{ color: "red" }}></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="col-sm-6">
                                            <div className="form-group">
                                                <span className="form-label">Voksen</span>

                                                <div className="form-control-sm-6">
                                                    <input value="0" type="number" id="antVoksenBiletter"/>
                                                    <div id="feilAntallVoksne" style={{ color: "red" }}></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="form-btn"></div>
                                    <div id="feil" style={{ color: "red" }}></div>
                                    <button className="submit-btn" /*onClick={e => getDestinasjon(e, 'Oslo')}*/ >Sjekk tilgjenglighet</button>
                                    </form>
                                </div>
                             </div>
                        </div>
                    </div>
                </div>
            </div>         
  );
}
