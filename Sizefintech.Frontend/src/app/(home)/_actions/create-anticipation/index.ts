"use server";

import { api } from "@/_lib/axios";
import { AxiosError } from "axios";
import { revalidatePath } from "next/cache";

interface InvoiceShortProps {
  number: string;
  grossAmount: number;
  dueDate: string;
}

interface InvoiceProps {
  number: string;
  netAmount: number;
  grossAmount: number;
  dueDate: string;
}

interface AnticipationRegister {
  invoices: InvoiceShortProps[];
}

interface AnticipationFullProps {
  cnpj: string;
  company: string;
  limit: number;
  netTotal: number;
  grossTotal: number;
  invoices: InvoiceProps[];
  createdAt: string;
}

export interface AnticipationResponse {
  success: boolean;
  errorMessages?: string[];
  data?: AnticipationFullProps;
}

export async function createAnticipation({ invoices }: AnticipationRegister) {
  try {
    const response = await api.post("/anticipations", {
      invoices,
    });

    revalidatePath("/");

    return { success: true, data: response.data } as AnticipationResponse;
  } catch (error: unknown) {
    if (error instanceof AxiosError) {
      return {
        success: false,
        ...(error.response?.data || ["Erro desconhecido"]),
      } as AnticipationResponse;
    }

    return {
      success: false,
      errorMessages: ["Erro desconhecido"],
    } as AnticipationResponse;
  }
}
