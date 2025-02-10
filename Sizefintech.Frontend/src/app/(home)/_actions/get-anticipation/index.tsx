"use server";

import { api } from "@/_lib/axios";
import { AxiosError } from "axios";
import { NextResponse } from "next/server";
import type { AnticipationResponse } from "../create-anticipation";

export async function getAnticipation(anticipationId: number) {
  try {
    const response = await api.get(`/anticipations/${anticipationId}`);

    if (response.status === 200) {
      return { success: true, data: response.data } as AnticipationResponse;
    }

    return NextResponse.json({ success: true });
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
