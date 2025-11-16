import axios from "axios";

export const defaultHeaders = {
    "Content-Type": "application/json",
};

export const AiPlaygroundApiClient = axios.create({
    baseURL: "https://localhost:7091/api/",
    headers: defaultHeaders,
});