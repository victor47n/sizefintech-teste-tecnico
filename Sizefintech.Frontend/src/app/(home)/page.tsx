import { RegisterAnticipationForm } from "./_components/register-anticipation-form";

export default function HomePage() {
  return (
    <div className="grid min-h-svh grid-cols-3 grid-rows-1 items-center justify-center gap-6 bg-muted p-6 md:p-10">
      <RegisterAnticipationForm />
      <div className="items-center justify-center">
        <p className="col-span-2">Home Page Content</p>
      </div>
    </div>
  );
}
