'use client';
import Header from "@/components/custom/header";
import RecordTable from "@/components/custom/record-table";
import { Toaster } from "@/components/ui/toaster";

export default function Home() {
  return (
    <div>
      <Toaster></Toaster>
      <Header></Header>
      <RecordTable></RecordTable>
    </div>
  );
}
