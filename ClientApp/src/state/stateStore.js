import { create } from 'zustand';

const store = (set) => ({
    loading: false,
    countries: [],
    country: {},
    countrySummary: {},
    setLoading: (data) => set(() => ({ loading: data })),
    saveCountries: (data) => set(() => ({ countries: data })),
    saveCountry: (data) => set(() => ({ country: data })),
    saveCountrySummary: (data) => set(() => ({ countrySummary: data }))
});
const useStore = create(store);
export default useStore;