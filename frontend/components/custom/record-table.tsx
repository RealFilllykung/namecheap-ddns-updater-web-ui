import { useEffect } from "react";
import { Table, TableBody, TableHead, TableHeader, TableRow } from "../ui/table";
import RecordTableRow from "./record-table-row";

export default function RecordTable()
{
    const recordModelList = [
        {
            domain: "test.domain.com",
            password: "12345678",
            ip: "123.123.123.123"
        },
        {
            domain: "test2.domain.com",
            password: "87654321",
            ip: "111.111.111.111"
        }
    ]

    return(
        <Table>
            <TableHeader>
                <TableRow>
                    <TableHead>Domain</TableHead>
                    <TableHead>IP</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {recordModelList.map((recordModel) => (
                    <RecordTableRow key={recordModel.domain} domainInput={recordModel.domain} passwordInput={recordModel.password} ipInput={recordModel.ip}></RecordTableRow>
                ))}
            </TableBody>
        </Table>
    )
}