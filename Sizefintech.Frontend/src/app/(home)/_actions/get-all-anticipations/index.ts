"use server";

import { api } from "@/_lib/axios";
import { AxiosError } from "axios";
import { revalidatePath } from "next/cache";
import { NextResponse } from "next/server";

export async function getAllAnticipations() {
  try {
    const response = await api.get("/anticipations");

    revalidatePath("/");

    if (response.status === 200) {
      return NextResponse.json({ success: true, ...response.data });
    }

    return NextResponse.json({ success: true, anticipations: [] });
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
