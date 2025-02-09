import { NextResponse } from "next/server";
import { cookies } from "next/headers";
import { api } from "@/_lib/axios";
import { AxiosError } from "axios";

export async function POST(req: Request) {
  const { name, cnpj, monthlyRevenue, industryId } = await req.json();

  try {
    const response = await api.post("/user", {
      name,
      cnpj,
      monthlyRevenue,
      industryId,
    });

    const token = response.data.token;

    cookies().set("sizefintech_auth", token, {
      httpOnly: true,
      secure: process.env.NODE_ENV === "production",
      maxAge: 60 * 60 * 24, // Expira em 24h
      path: "/",
    });

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
