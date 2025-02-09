import { ClaimUsernameForm } from "./_components/claim-username-form";
import Logo from "../../../public/logo.svg";

export default function LoginPage() {
  return (
    <div className="flex min-h-svh flex-col items-center justify-center gap-6 bg-muted p-6 md:p-10">
      <div className="flex w-full flex-row items-center justify-center gap-16">
        <a href="#" className="flex flex-col gap-2 font-medium">
          <h3 className="text-4xl max-w-xl font-bold sm:text-5xl">
            Quer simular a antecipação dos seus recebíveis em minutos?
          </h3>
          <span className="mt-2 text-xl text-muted-foreground">
            Entre agora mesmo!
          </span>
        </a>
        <div className="flex flex-col justify-center gap-6 mt-[-5.1rem]">
          <Logo className="h-16" />
          <ClaimUsernameForm />
        </div>
      </div>
    </div>
  );
}
