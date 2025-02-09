"use server";

import { api } from "@/_lib/axios";
import { AxiosError } from "axios";
import { revalidatePath } from "next/cache";
import { NextResponse } from "next/server";
import type { InvoiceProps } from "../../_components/register-anticipation-form";

interface AnticipationRegister {
  invoices: InvoiceProps[];
}

export async function createAnticipation({ invoices }: AnticipationRegister) {
  try {
    console.log("Action");

    await api.post("/anticipations", {
      invoices,
    });

    console.log("depois do post action");
    revalidatePath("/");

    return NextResponse.json({ success: true });
  } catch (error: unknown) {
    if (error instanceof AxiosError) {
      return NextResponse.json(
        { ...error.response?.data },
        { status: error.response?.status }
      );
    }

    return NextResponse.json(
      { errorMessages: "Erro desconhecido" },
      { status: 500 }
    );
  }
}
