'use client';
import CreateNewRecordButton from "@/components/custom/create-new-record-button";
import Header from "@/components/custom/header";
import RecordTable from "@/components/custom/record-table";
import { Toaster } from "@/components/ui/toaster";
require('dotenv').config()

export default function Home() {
  return (
    <div>
      <Toaster></Toaster>
      <Header></Header>
      <CreateNewRecordButton></CreateNewRecordButton>
      <RecordTable></RecordTable>
    </div>
  );
}
