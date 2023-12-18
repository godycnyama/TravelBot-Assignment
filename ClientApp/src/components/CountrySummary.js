import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import useStore from '../state/stateStore';


export const CountrySummary = () => {
    const countrySummary = useStore((state) => state.countrySummary);
    return (
        <div className="container">
            <h2>Country Summary</h2>

            <table className="table">
                <tbody>
                    <tr>
                        <td>Name:</td>
                        <td>{countrySummary.name}</td>
                    </tr>
                    <tr>
                        <td>Capital:</td>
                        <td>{countrySummary.capital}</td>
                    </tr>
                    <tr>
                        <td>Population:</td>
                        <td>{countrySummary.population}</td>
                    </tr>
                    <tr>
                        <td>Latitude:</td>
                        <td>{countrySummary.latitude}</td>
                    </tr>
                    <tr>
                        <td>Longitude:</td>
                        <td>{countrySummary.longitude}</td>
                    </tr>
                    <tr>
                        <td>Number of languages:</td>
                        <td>{countrySummary.numberOfLanguages}</td>
                    </tr>
                    <tr>
                        <td>Cars drive on the:</td>
                        <td>{countrySummary.carsDriveOnSide}</td>
                    </tr>
                    <tr>
                        <td>Start of Week:</td>
                        <td>{countrySummary.startOfWeek}</td>
                    </tr>
                    <tr>
                        <td>Sunrise Time:</td>
                        <td>{countrySummary.sunrise}</td>
                    </tr>
                    <tr>
                        <td>Sunset Time:</td>
                        <td>{countrySummary.sunset}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
};
