import { AxiosError } from "axios";
import { AiPlaygroundApiClient } from "../base/BaseApiClient";
import { UserDto } from "../models/UserDto";

export const AuthApiClient = {
    urlPath: "users",

    async authenticateUser(userDto: UserDto): Promise<number> {
        return AiPlaygroundApiClient
            .post<number>(`${this.urlPath}/authenticate`, userDto)
            .then((response) => response.data)
            .catch((err: AxiosError) => {
                const errorResponse = err.response?.data;
                return Promise.reject(errorResponse);
            });
    },
};
