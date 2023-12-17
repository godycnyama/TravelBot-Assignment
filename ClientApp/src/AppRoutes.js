import { Countries } from "./components/Countries";
import { CountrySummary } from "./components/CountrySummary";
import { RandomCountry } from "./components/RandomCountry";

const AppRoutes = [
    {
        index: true, 
        element: <Countries />
    },
    {
        path: '/countrySummary',
        element: <CountrySummary />
    },
    {
        path: '/randomCountry',
        element: <RandomCountry />
    }
];

export default AppRoutes;
