import React from 'react';
import { Button } from 'reactstrap';
import useStore from '../state/stateStore';
import axiosClient from '../axiosClient';
import LoadingSkeleton from './LoadingSkeleton'; 



export const RandomCountry = () => {
    const saveCountry = useStore((state) => state.saveCountry);
    const country = useStore((state) => state.country);
    const setLoading = useStore((state) => state.setLoading);
    const loading = useStore((state) => state.loading);

    const getRandomCountry = async () => {
        setLoading(true);
        let response = await axiosClient.get("api/countries/randomCountry");
        saveCountry(response.data);
        setLoading(false)
    }
    return (
        <div className="container">
            <h2>Countries Summary</h2>
            <Button color="primary" onClick={() => getRandomCountry()}>
                Get random country
            </Button>
            <table className="table">
                {loading && <LoadingSkeleton />}
                {Object?.keys(country)?.length > 0 && (
                <tbody>
                    <tr>
                        <td>Name:</td>
                        <td>{country.name}</td>
                    </tr>
                    <tr>
                        <td>Capital:</td>
                        <td>{country.capital}</td>
                    </tr>
                    <tr>
                        <td>Population:</td>
                        <td>{country.population}</td>
                    </tr>
                    <tr>
                        <td>Latitude:</td>
                        <td>{country.latitude}</td>
                    </tr>
                    <tr>
                        <td>Longitude:</td>
                        <td>{country.longitude}</td>
                    </tr>
                    <tr>
                        <td>Number of languages:</td>
                        <td>{country.numberOfLanguages}</td>
                    </tr>
                    <tr>
                        <td>Cars drive on the:</td>
                        <td>{country.carsDriveOnSide}</td>
                    </tr>
                    <tr>
                        <td>Start of Week:</td>
                        <td>{country.startOfWeek}</td>
                    </tr>
                </tbody>
                )}
            </table>
        </div>
    );
};
