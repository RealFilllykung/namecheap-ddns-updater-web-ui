import React, { useState } from "react";
import { TableCell, TableRow } from "../ui/table";
import SaveButton from "./save-button";
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
            <TableCell><Input placeholder="Domain name" value={domain} onChange={ event => setDomain(event.target.value)}></Input></TableCell>
            <TableCell><Input placeholder="Password" type="password" value={password} onChange={ event => setPassword(event.target.value) }></Input></TableCell>
            <TableCell>{ipInput}</TableCell>
            <TableCell className="flex justify-center"><SaveButton></SaveButton></TableCell>
            <TableCell><DeleteButton></DeleteButton></TableCell>
        </TableRow>
    )
};

export default RecordTableRow;