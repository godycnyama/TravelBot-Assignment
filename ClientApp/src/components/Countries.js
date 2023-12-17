import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'reactstrap';
import useStore  from '../state/stateStore';
import axiosClient from '../axiosClient';
import LoadingSkeleton from './LoadingSkeleton'; 


export const Countries = () => {
    useEffect(() => {
        getTopFiveCountries();
    }, []);

    const countries = useStore((state) => state.countries);
    const saveCountrySummary = useStore((state) => state.saveCountrySummary);
    const saveCountries = useStore((state) => state.saveCountries);
    const setLoading = useStore((state) => state.setLoading);
    const loading = useStore((state) => state.loading);
    const navigate = useNavigate();

    const getTopFiveCountries = async () => {
        setLoading(true)
        let response = await axiosClient.get("api/countries/topfive");
        saveCountries(response.data);
        setLoading(false);
    }

    const getCountrySummary = async (country) => {
        setLoading(true)
        let response = await axiosClient.get(`api/countries/${country}`);
        saveCountrySummary(response.data);
        setLoading(false);
        navigate('/countrySummary');
    }

    return (
        <div className="container">
            <h2>Top 5 Countries</h2>
            <table className="table table-striped" aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Capital</th>
                        <th>Population</th>
                        <th>Number of Languages</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                {loading ? <LoadingSkeleton /> : (
                <tbody>
                    {countries?.map(country =>
                        <tr key={country?.name}>
                            <td>{country?.name}</td>
                            <td>{country?.capital}</td>
                            <td>{country?.population}</td>
                            <td>{country?.numberOfLanguages}</td>
                            <td>
                                <Button color="primary" onClick={() => getCountrySummary(country.name)}>
                                    View country summary
                                </Button>
                            </td>
                        </tr>
                    )}
                </tbody>) }

            </table>
        </div>
    );
};
