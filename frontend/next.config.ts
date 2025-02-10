import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: 'standalone',
  env:{
    PUBLIC_RECORD_API_URL: process.env.NEXT_PUBLIC_RECORD_API_URL
  }
};

export default nextConfig;
