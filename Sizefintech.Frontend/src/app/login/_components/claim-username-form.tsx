"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/_components/ui/form";
import { useForm } from "react-hook-form";

import { z } from "zod";
import { Button } from "@/_components/ui/button";
import { ArrowRightIcon } from "lucide-react";
import { useRouter } from "next/navigation";
import { Card, CardContent, CardFooter } from "@/_components/ui/card";
import { validateCNPJ } from "@/_utils/validateCNPJ";
import { CNPJInput } from "@/_components/cnpj-input";
import { toast } from "sonner";

const formSchema = z.object({
  cnpj: z
    .string()
    .min(14, {
      message: "O CNPJ é obrigatório",
    })
    .max(18, "O CNPJ é inválido")
    .refine((cnpj) => validateCNPJ(cnpj), "O CNPJ é inválido"),
});

type FormSchema = z.infer<typeof formSchema>;

export function ClaimUsernameForm() {
  const form = useForm<FormSchema>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      cnpj: "",
    },
  });

  const router = useRouter();

  async function onSubmit(data: FormSchema) {
    const { cnpj } = data;

    const response = await fetch("/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ cnpj: cnpj.replace(/\D/g, "") }),
    });

    if (!response.ok) {
      const data = await response.json();
      toast.error(data.errorMessages);
      return;
    }

    router.push(`/`);
  }

  return (
    <Card className="min-w-[400px]">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <CardContent className="mt-4 grid grid-cols-2 gap-4">
            <FormField
              control={form.control}
              name="cnpj"
              render={({ field }) => (
                <FormItem className="col-span-2">
                  <FormLabel className="font-bold">CNPJ</FormLabel>
                  <FormControl>
                    <CNPJInput
                      format={""}
                      {...field}
                      onValueChange={(values) => field.onChange(values.value)}
                      placeholder="Informe seu CNPJ"
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </CardContent>
          <CardFooter className="flex flex-col gap-4">
            <Button
              type="submit"
              className="font-medium w-full"
              disabled={form.formState.isSubmitting}
            >
              Entrar
              <ArrowRightIcon />
            </Button>
            <div className="text-center text-sm">
              Ainda não tem uma conta?{" "}
              <a
                href="/register"
                className="text-green-500 underline underline-offset-4 hover:text-green-700"
              >
                Crie uma agora
              </a>
            </div>
          </CardFooter>
        </form>
      </Form>
    </Card>
  );
}
