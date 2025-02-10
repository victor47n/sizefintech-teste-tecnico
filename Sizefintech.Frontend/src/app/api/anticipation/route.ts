import { NextResponse } from "next/server";
import { api } from "@/_lib/axios";
import { AxiosError } from "axios";

export async function GET(req: Request) {
  const { anticipationId } = await req.json();

  try {
    const response = await api.get(`/anticipations/${anticipationId}`);

    return NextResponse.json({ success: true, ...response.data });
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
