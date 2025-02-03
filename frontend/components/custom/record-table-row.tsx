import React, { useState } from "react";
import { TableCell, TableRow } from "../ui/table";
import EditButton from "./edit-button";
import DeleteButton from "./delete-button";
import { Input } from "../ui/input";

interface RecordTableRowProps {
    domainInput: string;
    passwordInput: string;
    ipInput: string;
}

const RecordTableRow: React.FC<RecordTableRowProps> = ({domainInput, passwordInput, ipInput}) => {
    const [domain, setDomain] = useState(domainInput)
    const [password, setPassword] = useState(passwordInput)
    return(
        <TableRow>
            <TableCell>{domainInput}</TableCell>
            <TableCell>{ipInput}</TableCell>
            <TableCell className="flex justify-center"><EditButton></EditButton></TableCell>
            <TableCell><DeleteButton></DeleteButton></TableCell>
        </TableRow>
    )
};

export default RecordTableRow;