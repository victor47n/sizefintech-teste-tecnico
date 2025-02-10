"use server";

import { api } from "@/_lib/axios";
import { AxiosError } from "axios";

export interface User {
  name: string;
  cnpj: string;
  monthlyRevenue: number;
  industryId: number;
  limit: number;
}

export interface UserResponse {
  success: boolean;
  errorMessages?: string[];
  data?: User;
}

export async function getLoggedUser() {
  try {
    const response = await api.get("/user");

    return { success: true, data: response.data } as UserResponse;
  } catch (error: unknown) {
    if (error instanceof AxiosError) {
      return {
        success: false,
        ...(error.response?.data || ["Erro desconhecido"]),
      } as UserResponse;
    }

    return {
      success: false,
      errorMessages: ["Erro desconhecido"],
    } as UserResponse;
  }
}
