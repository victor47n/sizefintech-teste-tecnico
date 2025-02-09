import type { Metadata } from "next";
import { Mulish } from "next/font/google";
import { Toaster } from "@/_components/ui/sonner";
import "./globals.css";

const mulish = Mulish({
  subsets: ["latin-ext"],
  variable: "--font-mulish",
  weight: ["400", "500", "700"],
});

export const metadata: Metadata = {
  title: "Size FinTech",
  description: "",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${mulish.variable} antialiased`}>
        {children}
        <Toaster richColors position="top-right" />
      </body>
    </html>
  );
}
